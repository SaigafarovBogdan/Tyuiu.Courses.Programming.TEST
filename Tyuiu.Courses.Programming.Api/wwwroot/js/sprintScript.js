const tasks = [
    {
        option: 'Жопа',
        taskName: 'Task 10',
        task: 'Написать программу, которая вычисляет выражение 36/2/9+1-6/2*3 и печатает результат на экране.',
        imageSrc: 'Assets/Image/1.7.1.png',
        sub: 'Название проектов (консольного приложения, библиотеки классов, тестового модуля) оформить по шаблону в соответствии с вариантом. Интерфейс консольного приложения оформить по шаблону.'
    }
]

var h2Element = document.querySelector('.col-md-12 h2');
var h5Element = document.querySelector('.col-md-12 h5');
var p1Element = document.querySelector('.col-md-12 p:nth-of-type(1)');
var divElement = document.querySelector('.col-md-12 div');
var p2Element = document.querySelector('.col-md-12 p:nth-of-type(2)');

var text1Element = document.querySelector('.text1');
var text2Element = document.querySelector('.text2');
var rowElement = document.querySelector('.row');
var btnSprintComplete = document.querySelector('.sprintComplete');

var inputLink = document.querySelector('.hstack input');
var btnElement = document.querySelector('.hstack button');
var inputGroup = document.querySelector('.hstack');

var task = tasks[0];


h2Element.textContent = task.option;
h5Element.textContent = task.taskName;
p1Element.textContent = task.task;

p2Element.textContent = task.sub;

//Добавление изображения в таск
function addImage(){
  if(task.imageSrc != null){
    const img = document.createElement('img');
    img.classList.add('mt-4');
    img.classList.add('ms-5');
    img.classList.add('object-fit-lg-contain');
    img.src = task.imageSrc;
    divElement.appendChild(img);
}
}

addImage();

//Проверка ссылки на правильность ввода и открытие доступа к таску
function isValid()
{
  const linkInput = document.getElementById('githubLinkInput');
  const value = linkInput.value;
  var httpRegex = /^https?:\/\/(?:www\.)?[-a-zA-Z0-9@:%._\+~#=]{1,256}\.[a-zA-Z0-9()]{1,6}\b(?:[-a-zA-Z0-9()@:%_\+.~#?&\/=]*)$/;
  return httpRegex.test(value);
}

function openTask(){
  if(isValid() === true && pattern() === true){
    text1Element.classList.remove('visually-hidden');
    text2Element.classList.remove('visually-hidden');
    rowElement.classList.remove('visually-hidden');
    btnSprintComplete.classList.remove('visually-hidden');
    inputGroup.style.display = 'none';
  }
  else{
    alert('Неверно введена ссылка на GitHub!');
    inputLink.value = '';
    console.log('invalid link');
  }
}

btnElement.addEventListener("click", openTask);

function pattern(){

const template = "https://github.com//Tyuiu..Sprint1";

const linkInput = document.getElementById('githubLinkInput');
const value = linkInput.value;

const fullName = value.slice(value.indexOf("Tyuiu.") + 6, value.indexOf(".Sprint1")).replace(/F|\./g, "");

const splitLink = value.split("/");

const individualLink = value.replace(splitLink[3],"").replace(fullName, "");

if (template === individualLink) {
  console.log("Ссылки совпадают");
  return true;
} else {
  console.log("Ссылки не совпадают");
  return false;
}
}