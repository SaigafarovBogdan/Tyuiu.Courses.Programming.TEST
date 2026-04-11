const sprintResult = document.querySelector('.sprintResult');
const testResult = document.querySelector('.testResult');
const h2Test = document.createElement('h2');
const h2Sprint = document.createElement('h2');

let sum = parseFloat(sessionStorage.getItem("task0")) + parseFloat(sessionStorage.getItem("task1")) + parseFloat(sessionStorage.getItem("task2")) + parseFloat(sessionStorage.getItem("task3")) + parseFloat(sessionStorage.getItem("task4")) + parseFloat(sessionStorage.getItem("task5")) + parseFloat(sessionStorage.getItem("task6")) + parseFloat(sessionStorage.getItem("task7"));

let maxSum = 0.6 * 8;

h2Test.textContent = 'Ваш результат за проверку знаний: ' + sessionStorage.getItem("score") + ' из ' + sessionStorage.getItem("maxScore");

h2Sprint.textContent = 'Ваш предварительный результат за Sprint 1: ' + sum + ' из ' + maxSum;

testResult.appendChild(h2Test);
sprintResult.appendChild(h2Sprint);

sessionStorage.setItem("task0", 0.6);
sessionStorage.setItem("task1", 0.6);
sessionStorage.setItem("task2", 0.2);
sessionStorage.setItem("task3", 0.2);
sessionStorage.setItem("task4", 0.6);
sessionStorage.setItem("task5", 0.6);
sessionStorage.setItem("task6", 0.2);
sessionStorage.setItem("task7", 0.6);
