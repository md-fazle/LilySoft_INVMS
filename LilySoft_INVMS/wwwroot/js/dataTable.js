document.addEventListener("DOMContentLoaded", function () {
    const table = document.getElementById("categoryTable");
    const searchInput = document.getElementById("tableSearch");
    const rows = Array.from(table.tBodies[0].rows);
    let currentPage = 1;
    const rowsPerPage = 10;

    // Function to display rows for current page
    function displayRows(filteredRows = null) {
        const start = (currentPage - 1) * rowsPerPage;
        const end = start + rowsPerPage;
        const displayList = filteredRows || rows;

        rows.forEach(row => row.style.display = "none"); // hide all

        displayList.slice(start, end).forEach(row => row.style.display = "");

        renderPagination(displayList.length);
    }

    // Render pagination buttons
    function renderPagination(totalRows) {
        let pagination = document.getElementById("tablePagination");
        if (!pagination) {
            pagination = document.createElement("div");
            pagination.id = "tablePagination";
            pagination.style.marginTop = "15px";
            pagination.style.textAlign = "center";
            table.parentNode.appendChild(pagination);
        }
        pagination.innerHTML = "";

        const pageCount = Math.ceil(totalRows / rowsPerPage);
        for (let i = 1; i <= pageCount; i++) {
            const btn = document.createElement("button");
            btn.textContent = i;
            btn.className = "btn btn-sm me-1 mb-1";
            btn.style.border = "1px solid #0d6efd";
            btn.style.background = i === currentPage ? "#0d6efd" : "#fff";
            btn.style.color = i === currentPage ? "#fff" : "#0d6efd";
            btn.addEventListener("click", () => {
                currentPage = i;
                displayRows(currentFilteredRows);
            });
            pagination.appendChild(btn);
        }
    }

    let currentFilteredRows = rows;

    // Search Filter (works for both numbers and text)
    searchInput.addEventListener("input", function () {
        const query = this.value.trim().toLowerCase();

        currentFilteredRows = rows.filter(row => {
            return Array.from(row.cells).some(cell => {
                return cell.textContent.toLowerCase().includes(query);
            });
        });

        currentPage = 1; // reset to first page
        displayRows(currentFilteredRows);
    });

    // Simple Sort
    table.querySelectorAll("th").forEach((header, index) => {
        let asc = true;
        header.style.cursor = "pointer";
        header.addEventListener("click", function () {
            const sortedRows = (currentFilteredRows || rows).sort((a, b) => {
                const aText = a.cells[index].textContent.trim().toLowerCase();
                const bText = b.cells[index].textContent.trim().toLowerCase();

                // handle numbers
                const aNum = parseFloat(aText);
                const bNum = parseFloat(bText);

                if (!isNaN(aNum) && !isNaN(bNum)) {
                    return asc ? aNum - bNum : bNum - aNum;
                } else {
                    return asc ? aText.localeCompare(bText) : bText.localeCompare(aText);
                }
            });

            asc = !asc;
            currentFilteredRows = sortedRows;
            currentPage = 1;
            displayRows(sortedRows);
        });
    });

    // Initial display
    displayRows();
});
