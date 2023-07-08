<?php

require __DIR__ . "/../../folder_library/json-response.php";
require __DIR__ . "/../../folder_library/request.php";
require __DIR__ . "/../../models/users.php";
require __DIR__ . "/../../models/badge.php";

$json = Request::getJsonBody();


$user = UserModel::getBylogin($json["username"]);



if (!$user) {
    Response::json(400, [], ["success" => false, "error" => "Identifiant incorrect"]);
    die();
}



if ($json["password"] !== $user["password"]) {
    Response::json(400, [], ["success" => false, "error" => "Identifiant incorrect"]);
    die();
}


$token = bin2hex(random_bytes(16));

$user["token"] = $token;



UserModel::updateById($user);

$obtain = UserModel::getBadge($user["id"]);

$badge = [];
if(!$obtain){
    Response::json(200, [], ["success" => true, "token" => $token, "username" => $json["username"], "prenium"=> (bool)$user["noPub"],"badge"=>$badge]);
    die();
}

foreach ($obtain as $row){
    $badgeID = $row['badgeID'];
    $badge = array_merge($badge,BadgeModel::getBadgeUser($badgeID));
}

Response::json(200, [], ["success" => true, "token" => $token, "username" => $json["username"], "prenium"=> (bool)$user["noPub"],"badge"=>$badge]);
