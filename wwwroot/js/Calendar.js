document.addEventListener("DOMContentLoaded", function () {
    console.log("Script loaded");  // To verify script is running
    let currentDate = new Date();
    const calendarBody = document.getElementById("calendar-body");

    function loadCalendar(year, month) {
        calendarBody.innerHTML = "";

        // Update month and year display
        document.querySelector(".current-month").innerText =
            new Date(year, month - 1).toLocaleString("default", { month: "long", year: "numeric" });

        const firstDayOfMonth = new Date(year, month - 1, 1).getDay();
        const daysInMonth = new Date(year, month, 0).getDate();

        let dayCounter = 1;
        let row = document.createElement("tr");

        // Fill initial empty cells before the start of the month
        for (let i = 0; i < (firstDayOfMonth + 6) % 7; i++) {
            row.appendChild(document.createElement("td"));
        }

        // Fill in days of the month
        while (dayCounter <= daysInMonth) {
            if (row.children.length === 7) {
                calendarBody.appendChild(row);
                row = document.createElement("tr");
            }

            const cell = document.createElement("td");
            cell.classList.add("attendance-day");

            const dayNumber = document.createElement("span");
            dayNumber.classList.add("day-number");
            dayNumber.innerText = dayCounter;

            const attendanceStatus = document.createElement("span");
            attendanceStatus.classList.add("attendance-status");

            // Placeholder for attendance status; replace with actual data
            const status = Math.random() > 0.5 ? "Present" : "Absent";  // Replace with real data
            attendanceStatus.innerText = status;
            cell.classList.add(status.toLowerCase());

            cell.appendChild(dayNumber);
            cell.appendChild(document.createElement("br"));
            cell.appendChild(attendanceStatus);

            row.appendChild(cell);
            dayCounter++;
        }

        // Add remaining empty cells at the end of the last row
        while (row.children.length < 7) {
            row.appendChild(document.createElement("td"));
        }
        calendarBody.appendChild(row);
    }

    loadCalendar(currentDate.getFullYear(), currentDate.getMonth() + 1);

    // Event listeners for Previous and Next buttons
    document.getElementById("prev-month").addEventListener("click", function (event) {
        event.preventDefault();  // Prevent form submission
        currentDate.setMonth(currentDate.getMonth() - 1);
        loadCalendar(currentDate.getFullYear(), currentDate.getMonth() + 1);
    });

    document.getElementById("next-month").addEventListener("click", function (event) {
        event.preventDefault();  // Prevent form submission
        currentDate.setMonth(currentDate.getMonth() + 1);
        loadCalendar(currentDate.getFullYear(), currentDate.getMonth() + 1);
    });
});