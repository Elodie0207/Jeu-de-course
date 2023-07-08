<?php

require __DIR__ . "/../../folder_library/json-response.php";
require __DIR__ . "/../../folder_library/request.php";
require __DIR__ . "/../../models/users.php";


$token = Request::getHeader("token");
$user = UserModel::getByToken($token);

if (!$user) {
    Response::json(400, [], ["success" => false, "error" => "Bad request"]);
    die();
}

$user["nbPub"] += 1;


UserModel::updateById($user);


Response::json(200, [], ["success" => true,"nbPub"=>$user["nbPub"]]);
