<?php
ini_set("display_errors", 1);
error_reporting(E_ALL);

$route = $_REQUEST["route"] ?? "undefined";
$method = $_SERVER["REQUEST_METHOD"];


if ($route === "logout" && $method === "POST") {

    include __DIR__ . "/controllers/logout/logout.php";
    die();
}

else if ($route === "login" && $method === "POST") {
    include __DIR__ . "/controllers/login/login.php";

    die();
}
else if($route === "users"){
    if($method === "GET")
    {
        include __DIR__ . "/controllers/users/get.php";

        die();
    }

    if($method === "POST")
    {
        include __DIR__ . "/controllers/users/create.php";

        die();
    }
}

else if ($route === "premium" && $method === "POST") {
    include __DIR__ . "/controllers/users/premium.php";

    die();
}


else if($route === "pub"){
    if($method === "POST")
    {
        include __DIR__ . "/controllers/pub/create.php";

        die();
    }

    if($method === "GET")
    {
        include __DIR__ . "/controllers/pub/get.php";

        die();
    }
}

else if($route === "get_1_pub"){

    if($method === "GET")
    {
        include __DIR__ . "/controllers/pub/get_1_pub.php";

        die();
    }
}

else if($route === "get_1_banniere"){

    if($method === "GET")
    {
        include __DIR__ . "/controllers/pub/get_1_banniere.php";

        die();
    }
}

else if($route === "badge"){
    if($method === "POST")
    {
        include __DIR__ . "/controllers/badge/create.php";

        die();
    }

    if($method === "GET")
    {
        include __DIR__ . "/controllers/badge/get.php";

        die();
    }
}

else if($route === "like"){
    if($method === "POST")
    {
        include __DIR__ . "/controllers/like/create.php";

        die();
    }
}
else if($route === "updateNbPub"){
    if($method === "GET")
    {
        include __DIR__ . "/controllers/users/nbPub.php";

        die();
    }
}

else if($route === "updateBadge"){
    if($method === "POST")
    {
        include __DIR__ . "/controllers/users/updateBadge.php";

        die();
    }
}


?>

