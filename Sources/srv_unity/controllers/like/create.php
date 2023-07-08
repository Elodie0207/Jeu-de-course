<?php

require __DIR__ . "/../../folder_library/json-response.php";
require __DIR__ . "/../../models/like.php";
require __DIR__ . "/../../models/users.php";
require __DIR__ . "/../../folder_library/request.php";

try {
    $json = Request::getJsonBody();

    $token = Request::getHeader("token");
    $user = UserModel::getByToken($token);
    $like = UserModel::getPubLike($user["id"]);

    $json["userID"] = $user["id"];

    if (!empty($like)) {
        foreach ($like as $row) {
            if ($row['themeID'] == $json["themeID"]) {
                LikeModel::delete($json);
                Response::json(202, [], ["success" => true, "like" => false]);
                die();
            }
        }
    }

    if ($token) {
        LikeModel::create($json);

        $nbLike = count(UserModel::getPubLike($json["userID"]));

        $json["nameBadge"] = "badge_like";

        $isAlreadyBadged = UserModel::VerifBadgeUserName($json);
        if ($isAlreadyBadged) {
            $isFirst = false;
        } else {
            $isFirst = true;
        }

        if ($isFirst) {
            $json["badgeID"] = 6;
            UserModel::createBadge($json);
        }

        Response::json(201, [], ["success" => true, "like" => true, "nbLike" => $nbLike, "isFirst" => $isFirst]);
        die();
    }
} catch (PDOException $exception) {
    $errorMessage = $exception->getMessage();
    Response::json(500, [], ["error" => "Error while accessing the database: $errorMessage"]);
}
?>
