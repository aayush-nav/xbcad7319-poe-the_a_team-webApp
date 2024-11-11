document.addEventListener("DOMContentLoaded", function () {
    const tabs = document.querySelectorAll(".tab-link");
    const tabContent = document.querySelectorAll(".tab-content");

    tabs.forEach((tab) => {
        tab.addEventListener("click", function () {
            tabs.forEach((btn) => btn.classList.remove("active"));
            tab.classList.add("active");

            const target = tab.dataset.tab;

            tabContent.forEach((content) => {
                content.style.display = "none";
            });

            document.getElementById(target).style.display = "block";
        });
    });
});
