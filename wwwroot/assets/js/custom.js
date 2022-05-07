$(document).on("click", "#productdetail", function (e) {
    e.preventDefault();

    let url = $(this).attr("href");

    fetch(url).then(response => response.text())
        .then(data => {
            $(".modal-content").html(data)
            Console.log("slm");

            $("#quick_view").modal("show")
            Console.log("asagi bled");

        })
})