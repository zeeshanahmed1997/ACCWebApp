document.addEventListener('DOMContentLoaded', function () {
    var navigationSwitch = document.querySelector('.navigation-switch');
    var navigationBar = document.querySelector('.navigation');
    var contentContainer = document.querySelector('.content-container');

    navigationSwitch.addEventListener('change', function () {
        navigationBar.classList.toggle('hidden');

        contentContainer.classList.toggle('hidden');
    });
});