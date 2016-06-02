function uploader()
{
    document.getElementById(image1).innerHTML = "~/images/mydp.jpg";


    document.getElementById("image1").innerHTML = "~/images/mydp.jpg";

    document.location.href = "localhost:18303/Home/Article";
}





function logout_visibility()
{
    if (document.getElementById("logout").style.visibility = "hidden") {
        document.getElementById("logout").style.visibility = "visible";
    }
    else if (document.getElementById("logout").style.visibility = "visible") {
        document.getElementById("logout").style.visibility = "hidden";
    }
}