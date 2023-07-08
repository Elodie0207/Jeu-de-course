<?php

require __DIR__ . "/../../folder_library/json-response.php";
require __DIR__ . "/../../folder_library/request.php";
require __DIR__ . "/../../models/users.php";


$json = Request::getJsonBody();


$user = UserModel::getByToken($json["token"]);

if (!$user) {
    Response::json(400, [], ["success" => false, "error" => "Bad request"]);
    die();
}

$user["noPub"] = $json["premium"];

$json["userID"] = $user["id"];
$json["nameBadge"] = "badge_like";

$isAlreadyBadged = UserModel::VerifBadgeUserName($json);
if ($isAlreadyBadged) {
    $isFisrt =false;
}
else{
    $isFisrt = true;
}

UserModel::updateById($user);

Response::json(200, [], ["success" => true,"premium"=>$json["premium"],"isFirst"=>$isFisrt]);
