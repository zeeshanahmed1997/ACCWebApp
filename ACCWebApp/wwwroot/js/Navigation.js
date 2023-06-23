// Get all collapse buttons
const collapseBtns = document.querySelectorAll('.collapse-btn');

// Attach click event listeners to each collapse button
collapseBtns.forEach((btn) => {
    btn.addEventListener('click', () => {
        const parentItem = btn.parentElement;
        const subitems = parentItem.querySelector('.subitems');
        subitems.classList.toggle('collapsed');
    });
});