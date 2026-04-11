function countdownTimer(time) {
  var timerContainer = document.getElementById("timer");

  var hours = Math.floor(time / 3600);
  var minutes = Math.floor((time % 3600) / 60);
  var seconds = Math.floor(time % 60);

  hours = hours.toString().padStart(2, '0');
  minutes = minutes.toString().padStart(2, '0');
  seconds = seconds.toString().padStart(2, '0');

  timerContainer.innerHTML = 'Осталось времени: ' + hours + ':' + minutes + ':' + seconds;

  if (time > 0) {
    time--;
    setTimeout(function() {
      countdownTimer(time);
    }, 1000);
  } else {
    alert("Время вышло!");
  }
}

var timeInSeconds = 0 * 3600 + 2 * 60 + 0;

countdownTimer(timeInSeconds);