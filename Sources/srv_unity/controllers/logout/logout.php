<?php

require __DIR__ . "/../../library/json-response.php";
require __DIR__ . "/../../library/request.php";
require __DIR__ . "/../../models/users.php";


$json = Request::getJsonBody();


$user = UserModel::getByToken($json["token"]);

if (!$user) {
    Response::json(400, [], ["success" => false, "error" => "Bad request"]);
    die();
}

$user["token"] = null;


UserModel::updateById($user);


Response::json(200, [], ["success" => true]);
