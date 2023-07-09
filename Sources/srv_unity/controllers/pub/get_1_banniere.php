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
            $pubs = [];
            foreach ($like as $row){
                $likeID = $row['themeID'];
                $pubs = array_merge($pubs,PubModel::getPubUserBannierre($likeID));
            }
            if(!empty($pubs)){
                $pbCount = count($like);
                $randPub = rand(1,$pbCount);



                $pub = $pubs[$randPub-1];

                Response::json(200, [], [ "id" => $pub["id"],"themeID" => $pub["themeID"],"websiteLink" => $pub["websiteLink"], "path" => $pub["lien"]]);
                die();
            }
        }

    }

    $pubs = PubModel::getAllBanniere();
    $pbCount = count($pubs);
    $randPub = rand(1,$pbCount);

    $pub = $pubs[$randPub-1];

    Response::json(200, [], [ "id" => $pub["id"],"themeID" => $pub["themeID"],"websiteLink" => $pub["websiteLink"], "path" => $pub["lien"]]);
    die();

} catch (PDOException $exception) {
    $errorMessage = $exception->getMessage();
    Response::json(500, [], [ "error" => "Error while accessing the database: $errorMessage" ]);
}