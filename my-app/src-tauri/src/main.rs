#![cfg_attr(not(debug_assertions), windows_subsystem = "windows")]

mod s7_client;
mod modbus_client;
mod database;
mod telegram;

use std::sync::Mutex;
use tauri::State;

struct AppState {
    s7_running: Mutex<bool>,
    modbus_running: Mutex<bool>,
    // Статистика
    s7_counters: Mutex<S7Counters>,
    modbus_counters: Mutex<ModbusCounters>,
}

struct S7Counters {
    counter_time: u32,
    counter_plc3679: u32,
    counter_plc2: u32,
    counter_messages: u32,
    counter_db: u32,
    counter_s7: u32,
    time_update: u128,
    time_plc3679: u128,
    time_plc2: u128,
    time_messages: u128,
    time_write: u128,
    time_last_cycle: u128,
}

struct ModbusCounters {
    counter_time: u32,
    counter_packaging: u32,
    counter_blo: u32,
    counter_vao: u32,
    counter_energy: u32,
    counter_filtr: u32,
    counter_electro: u32,
    counter_db: u32,
    counter_mb: u32,
    time_update: u128,
    time_packaging: u128,
    time_blo: u128,
    time_vao: u128,
    time_energy: u128,
    time_filtr: u128,
    time_electro: u128,
    time_write: u128,
    time_last_cycle: u128,
}

#[tauri::command]
async fn start_s7_polling(state: State<'_, AppState>) {
    let mut running = state.s7_running.lock().unwrap();
    *running = true;
    
    // Запуск фонового цикла (как BackgroundWorker)
    tauri::async_runtime::spawn(async move {
        s7_polling_loop(state).await;
    });
}

#[tauri::command]
fn stop_s7_polling(state: State<'_, AppState>) {
    let mut running = state.s7_running.lock().unwrap();
    *running = false;
}

#[tauri::command]
fn get_s7_status(state: State<'_, AppState>) -> S7Counters {
    state.s7_counters.lock().unwrap().clone()
}

// Аналогично для Modbus...

#[tauri::command]
async fn send_test_telegram(message_id: i32) -> Result<(), String> {
    telegram::send_message(message_id).await.map_err(|e| e.to_string())
}

fn main() {
    tauri::Builder::default()
        .manage(AppState {
            s7_running: Mutex::new(false),
            modbus_running: Mutex::new(false),
            s7_counters: Mutex::new(S7Counters { /* init */ }),
            modbus_counters: Mutex::new(ModbusCounters { /* init */ }),
        })
        .invoke_handler(tauri::generate_handler![
            start_s7_polling,
            stop_s7_polling,
            get_s7_status,
            start_modbus_polling,
            stop_modbus_polling,
            get_modbus_status,
            send_test_telegram
        ])
        .run(tauri::generate_context!())
        .expect("error while running tauri application");
}