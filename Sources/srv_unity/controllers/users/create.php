<?php

require __DIR__ . "/../../library/json-response.php";
require __DIR__ . "/../../models/users.php";
require __DIR__ . "/../../library/request.php";

try {
    $json = Request::getJsonBody();

    $user = UserModel::getBylogin($json["username"]);

    if ($user) {
        Response::json(400, [], ["success" => false, "error" => "taken"]);
        die();
    }

    $userId = UserModel::create($json);

    Response::json(201, [], [ "success" => true, "user_id" => $userId ]);
} catch (PDOException $exception) {
    $errorMessage = $exception->getMessage();
    Response::json(500, [], [ "error" => "Error while accessing the database: $errorMessage" ]);
}
