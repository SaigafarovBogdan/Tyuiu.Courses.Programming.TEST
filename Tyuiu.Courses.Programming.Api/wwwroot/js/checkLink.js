var btnElement = document.querySelector('.hstack button');
var inputLink = document.querySelector('.hstack input');

function isValid()
{
  const linkInput = document.getElementById('githubLinkInput');
  const value = linkInput.value;
  var httpRegex = /^https?:\/\/(?:www\.)?[-a-zA-Z0-9@:%._\+~#=]{1,256}\.[a-zA-Z0-9()]{1,6}\b(?:[-a-zA-Z0-9()@:%_\+.~#?&\/=]*)$/;
  return httpRegex.test(value);
}


function pattern(){

    const template = "https://github.com//Tyuiu..Sprint1";
    
    const linkInput = document.getElementById('githubLinkInput');
    const value = linkInput.value;
    
    const fullName = value.slice(value.indexOf("Tyuiu.") + 6, value.indexOf(".Sprint1")).replace(/F|\./g, "");
    
    const splitLink = value.split("/");
    
    const individualLink = value.replace(splitLink[3], "").replace(fullName, "");

	return template === individualLink;
}

function unlockSprint() {
	if (isValid() === true && pattern() === true) {
		return true;
	}
	else {
		alert('Неверно введена ссылка на репозиторий GitHub!');
		inputLink.value = '';
		console.log('invalid link');
		return false;
	}
}

btnElement.addEventListener("click", function onClick(){
    if(unlockSprint()){
        window.location.assign("sprint.html");
        console.log("переход");
    }
    else{
        console.log("gg");
    }
});