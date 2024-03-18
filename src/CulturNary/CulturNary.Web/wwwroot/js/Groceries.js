const addChecklist = document.getElementById('addChecklist');
const showAddChecklist = document.getElementById('showAddChecklist');
const input = addChecklist.querySelector('input');
const ul = document.querySelector('.grocery-list ul');

addChecklist.style.display = "none";

function showChecklist() {
    showAddChecklist.style.display = "none";
    addChecklist.style.display = "";
}

const checkmark = addChecklist.querySelector('a');
checkmark.addEventListener('click', addListItem);

function addListItem() {
    const text = input.value.trim();

    if (text !== "") {
        const li = document.createElement('li');
        li.textContent = text;
        ul.appendChild(li);
        li.addEventListener('click', toggleIdAttribute);

        input.value = "";
        saveListItems();
        addChecklist.style.display = "none";
        showAddChecklist.style.display = "";
    }
}

function saveListItems() {
    const listItems = Array.from(ul.getElementsByTagName('li')).map(li => li.textContent);
    localStorage.setItem('items', JSON.stringify(listItems));
}

function loadListItems() {
    const savedItems = localStorage.getItem('items');

    if (savedItems) {
        const items = JSON.parse(savedItems);
        items.forEach(item => {
            const li = document.createElement('li');
            li.textContent = item;
            ul.appendChild(li);
            li.addEventListener('click', toggleIdAttribute);
        });
    }
}

loadListItems();

function toggleIdAttribute(event) {
    const li = event.target;
    const currentId = li.getAttribute('id');

    if (currentId === 'done') {
        li.removeAttribute('id');
    } else {
        li.setAttribute('id', 'done');
    }
}

document.getElementById('checklistDate').addEventListener('change', loadListItems);

function saveListItems() {
    const date = document.getElementById('checklistDate').value;
    if (!date) {
        alert("Please select a date.");
        return;
    }

    const listItems = Array.from(ul.getElementsByTagName('li')).map(li => li.textContent);
    localStorage.setItem(date, JSON.stringify(listItems));
}

function loadListItems() {
    ul.innerHTML = '';
    const date = document.getElementById('checklistDate').value;

    if (!date) {
        console.log("No date selected.");
        return;
    }

    const savedItems = localStorage.getItem(date);

    if (savedItems) {
        const items = JSON.parse(savedItems);
        items.forEach(item => {
            const li = document.createElement('li');
            li.textContent = item;
            ul.appendChild(li);
            li.addEventListener('click', toggleIdAttribute);
        });
    }
}

function resetList() {
    const date = document.getElementById('checklistDate').value;
    if (confirm("Do you want to reset the list for " + date + "?")) {
        localStorage.removeItem(date);
        loadListItems();
    }
}
