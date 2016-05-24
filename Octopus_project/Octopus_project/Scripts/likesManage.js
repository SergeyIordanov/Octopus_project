function Like(id) {
    var element = document.getElementById('image_' + id);

    if (element.getAttribute("src") == "Content/like.png") {
        element.setAttribute("src", "Content/liked.png");
        element = document.getElementById("like_" + id);
        element.innerHTML = Number(element.innerHTML) + 1;
    }
    else {
        element.setAttribute("src", "Content/like.png");
        element = document.getElementById("like_" + id);
        element.innerHTML = Number(element.innerHTML) - 1;
    }
}