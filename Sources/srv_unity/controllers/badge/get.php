<?php

require __DIR__ . "/../../folder_library/json-response.php";
require __DIR__ . "/../../models/badge.php";
require __DIR__ . "/../../models/users.php";
require __DIR__ . "/../../folder_library/request.php";

try {
    $token = Request::getHeader("token");
    $user = UserModel::getByToken($token);

    if (!$user) {
        Response::json(401, [], ["success" => false, "error" => "Unauthorized"]);
        die();
    }

    $obtain = UserModel::getBadge($user["id"]);
    $badge = [];
    if(!$obtain){
        Response::json(200, [], [ "badge" => $badge ]);
        die();
    }

    foreach ($obtain as $row){
        $badgeID = $row['badgeID'];
        $badge = array_merge($badge,BadgeModel::getBadgeUser($badgeID));
    }
    Response::json(200, [], [ "badge" => $badge ]);

} catch (PDOException $exception) {
    $errorMessage = $exception->getMessage();
    Response::json(500, [], [ "error" => "Error while accessing the database: $errorMessage" ]);
}
