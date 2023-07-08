<?php

require_once  __DIR__ . "/../library/get-database-connection.php";

class LikeModel
{
    public static function create($like)
    {
        $connection = getDatabaseConnection();
        $createUserQuery = $connection->prepare("INSERT INTO aime(userID, themeID) VALUES(:userID, :themeID);");
        $createUserQuery->execute([
            "userID" => intval($like["userID"]),
            "themeID" => intval($like["themeID"])
        ]);
    }

    public static function delete($like)
    {
        $connection = getDatabaseConnection();
        $createUserQuery = $connection->prepare("DELETE FROM aime WHERE userID = :userID and themeID = :themeID");
        $createUserQuery->execute([
            "userID" => intval($like["userID"]),
            "themeID" => intval($like["themeID"])
        ]);
    }

}
