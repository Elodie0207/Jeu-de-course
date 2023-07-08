<?php

require __DIR__ . "/../../folder_library/json-response.php";
require __DIR__ . "/../../models/pub.php";
require __DIR__ . "/../../models/users.php";
require __DIR__ . "/../../folder_library/request.php";

try {
    $token = Request::getHeader("token");
    $user = UserModel::getByToken($token);

    if ($user) {
        $like = UserModel::getPubLike($user["id"]);

        if(!empty($like)){
            $pub = [];
            foreach ($like as $row){
                $likeID = $row['themeID'];
                $pub = array_merge($pub,PubModel::getPubUser($likeID));
            }

            Response::json(200, [], [ "pub" => $pub ]);
            die();
        }

    }

    $pub = PubModel::getAll();
    Response::json(200, [], [ "pub" => $pub ]);

} catch (PDOException $exception) {
    $errorMessage = $exception->getMessage();
    Response::json(500, [], [ "error" => "Error while accessing the database: $errorMessage" ]);
}
