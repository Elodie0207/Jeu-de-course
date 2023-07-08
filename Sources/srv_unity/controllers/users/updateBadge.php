<?php

require __DIR__ . "/../../library/json-response.php";
require __DIR__ . "/../../models/users.php";
require __DIR__ . "/../../models/badge.php";
require __DIR__ . "/../../library/request.php";

try {
    $json = Request::getJsonBody();

    $token = Request::getHeader("token");
    $user = UserModel::getByToken($token);
    $json["userID"] = $user["id"];


    $isAlreadyBadged = UserModel::VerifBadgeUserName($json);
    if (!$isAlreadyBadged) {
        $json["badgeID"] = BadgeModel::getBadgeIdWithName($json["nameBadge"]);
        UserModel::createBadge($json);
    }


    Response::json(202, [], ["success" => true]);
    die();

} catch (PDOException $exception) {
    $errorMessage = $exception->getMessage();
    Response::json(500, [], [ "error" => "Error while accessing the database: $errorMessage" ]);
}
