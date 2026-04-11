var testPage = document.querySelector('.testPage');
var sprintPage = document.querySelector('.sprintPage');
var resultPage = document.querySelector('.resultPage');


if(window.location.href.includes("theory.html") && sessionStorage.getItem('unlockTest') === 'true' 
&& sessionStorage.getItem('unlockSprint') === 'true' && sessionStorage.getItem('unlockResult') === 'true'){
  testPage.classList.remove('disabled');
  sprintPage.classList.remove('disabled');
  resultPage.classList.remove('disabled');
}
else if(window.location.href.includes("theory.html") && sessionStorage.getItem('unlockTest') === 'true' 
&& sessionStorage.getItem('unlockSprint') === 'true'){
  testPage.classList.remove('disabled');
  sprintPage.classList.remove('disabled');
}
else if(window.location.href.includes("theory.html") && sessionStorage.getItem('unlockTest') === 'true'){
  testPage.classList.remove('disabled');
}

if(window.location.href.includes("test.html") && sessionStorage.getItem('unlockSprint') === 'true' 
&& sessionStorage.getItem('unlockResult') === 'true'){
  sprintPage.classList.remove('disabled');
  resultPage.classList.remove('disabled');
}
else if(window.location.href.includes("test.html") && sessionStorage.getItem('unlockSprint') === 'true'){
  sprintPage.classList.remove('disabled');
}
else if(window.location.href.includes("test.html")){
  sessionStorage.setItem('unlockTest', true);
}

if(window.location.href.includes("sprint.html") && sessionStorage.getItem('unlockResult') === 'true'){ 
  resultPage.classList.remove('disabled');
}
else if(window.location.href.includes("sprint.html")){
  sessionStorage.setItem('unlockSprint', true);
}

if(window.location.href.includes("result.html")){
  sessionStorage.setItem('unlockResult', true);
}