import { invoke } from "@tauri-apps/api/core";

let s7Running = false;
let modbusRunning = false;

function switchTab(tabName) {
  document.querySelectorAll('.tab').forEach(t => t.classList.remove('active'));
  document.querySelectorAll('.tab-button').forEach(b => b.classList.remove('active'));
  
  document.getElementById(tabName).classList.add('active');
  event.target.classList.add('active');
}

async function startS7() {
  if (s7Running) return;
  s7Running = true;
  await invoke('start_s7_polling');
  pollS7Status();
}

async function stopS7() {
  s7Running = false;
  await invoke('stop_s7_polling');
}

async function startModbus() {
  if (modbusRunning) return;
  modbusRunning = true;
  await invoke('start_modbus_polling');
  pollModbusStatus();
}

async function stopModbus() {
  modbusRunning = false;
  await invoke('stop_modbus_polling');
}

async function testMessage() {
  await invoke('send_test_telegram', { messageId: 25 });
}

async function pollS7Status() {
  if (!s7Running) return;
  
  try {
    const status = await invoke('get_s7_status');
    document.getElementById('timeUpdate').textContent = `Время обнов. даты: ${status.time_update} мс Счётчик: ${status.counter_time}`;
    document.getElementById('timePLC3679').textContent = `Время PLC_3679: ${status.time_plc3679} мс Счётчик: ${status.counter_plc3679}`;
    document.getElementById('timePLC2').textContent = `Время PLC_2: ${status.time_plc2} мс Счётчик: ${status.counter_plc2}`;
    document.getElementById('timeMessages').textContent = `Время сообщений: ${status.time_messages} мс Счётчик: ${status.counter_messages}`;
    document.getElementById('timeWrite').textContent = `Время записи: ${status.time_write} мс Счётчик: ${status.counter_db}`;
    document.getElementById('timeLastCycle').textContent = `Время последнего цикла: ${status.time_last_cycle} мс Счётчик: ${status.counter_s7}`;
  } catch (e) {
    console.error('Error polling S7:', e);
  }
  
  setTimeout(pollS7Status, 1000);
}

async function pollModbusStatus() {
  if (!modbusRunning) return;
  
  try {
    const status = await invoke('get_modbus_status');
    document.getElementById('mbTimeUpdate').textContent = `Время обнов. даты: ${status.time_update} мс Счётчик: ${status.counter_time}`;
    document.getElementById('mbTimePackaging').textContent = `Время TH_Pack: ${status.time_packaging} мс Счётчик: ${status.counter_packaging}`;
    document.getElementById('mbTimeBLO').textContent = `Время TH_BLO: ${status.time_blo} мс Счётчик: ${status.counter_blo}`;
    document.getElementById('mbTimeVAO').textContent = `Время TH_VAO: ${status.time_vao} мс Счётчик: ${status.counter_vao}`;
    document.getElementById('mbTimeEnergy').textContent = `Время TH_EnBlock: ${status.time_energy} мс Счётчик: ${status.counter_energy}`;
    document.getElementById('mbTimeFiltr').textContent = `Время TH_Filtr: ${status.time_filtr} мс Счётчик: ${status.counter_filtr}`;
    document.getElementById('mbTimeElectro').textContent = `Время Electro: ${status.time_electro} мс Счётчик: ${status.counter_electro}`;
    document.getElementById('mbTimeWrite').textContent = `Время записи: ${status.time_write} мс Счётчик: ${status.counter_db}`;
    document.getElementById('mbTimeLastCycle').textContent = `Время последнего цикла: ${status.time_last_cycle} мс Счётчик: ${status.counter_mb}`;
  } catch (e) {
    console.error('Error polling Modbus:', e);
  }
  
  setTimeout(pollModbusStatus, 1000);
}