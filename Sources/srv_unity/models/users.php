<?php

require_once  __DIR__ . "/../library/get-database-connection.php";

class UserModel
{
    public static function getAll()
    {
        $connection = getDatabaseConnection();

        $getUsersQuery = $connection->query("SELECT * FROM users");

        $users = $getUsersQuery->fetchAll();

        return $users;
    }

    public static function aime($userID,$themeID)
    {
        $connection = getDatabaseConnection();
        $createUserQuery = $connection->prepare("INSERT INTO aime(userID, themeID) VALUES(:userID, :themeID);");
        $createUserQuery->execute([
            "userID" =>  intval($userID),
            "themeID" => intval($themeID)
        ]);
    }

    public static function create($user)
    {
        $connection = getDatabaseConnection();
        $createUserQuery = $connection->prepare("INSERT INTO users(username, password) VALUES(:username, :password);");
        $createUserQuery->execute([
            "username" => $user["username"],
            "password" => $user["password"]
        ]);
        $userId = $connection->lastInsertId();

        if (array_key_exists("themeID", $user)) {
            $themeID = intval($user["themeID"]);
            UserModel::aime($userId, $themeID);
        }

        return $userId;
    }




    public static function getBylogin($login)
    {
        $connection = getDatabaseConnection();
        $getUserByIdQuery = $connection->prepare("SELECT * FROM users WHERE username = :login;");
        $getUserByIdQuery->execute(["login" => $login]);

        return $getUserByIdQuery->fetch();
    }

    public static function getPubLike($id)
    {
        $connection = getDatabaseConnection();
        $getPubByIdQuery = $connection->prepare("SELECT themeID FROM aime WHERE userID = :id;");
        $getPubByIdQuery->execute([
            "id" => $id
        ]);

        return $getPubByIdQuery->fetchAll();

    }
    public static function getBadge($id)
    {
        $connection = getDatabaseConnection();
        $getBadgeByIdQuery = $connection->prepare("SELECT badgeID FROM badgeusr WHERE userID = :id;");
        $getBadgeByIdQuery->execute([
            "id" => $id
        ]);

        return $getBadgeByIdQuery->fetchAll();
    }


    public static function VerifBadgeUserName($badge)
    {
        $connection = getDatabaseConnection();
        $getBadgeByIdQuery = $connection->prepare("SELECT * FROM badge INNER JOIN badgeusr on badge.id = badgeusr.badgeID where badge.name = :badgeName and badgeusr.userID = :userID;");
        $getBadgeByIdQuery->execute([
            "badgeName" => $badge["nameBadge"],
            "userID" => $badge["userID"]
        ]);

        return $getBadgeByIdQuery->fetch();
    }

    public static function createBadge($info)
    {
        $connection = getDatabaseConnection();
        $createUserQuery = $connection->prepare("INSERT INTO badgeusr(userID, badgeID,date) VALUES(:userID, :badgeID,:date);");
        $createUserQuery->execute([
            "userID" => intval($info["userID"]),
            "badgeID" => intval($info["badgeID"]),
            "date" => date('Y-m-d')
        ]);
    }

    public static function getById($id)
    {
        $connection = getDatabaseConnection();
        $getUserByIdQuery = $connection->prepare("SELECT * FROM users WHERE id = :id;");

        $getUserByIdQuery->execute(
            [
            "id" => $id
            ]
        );

        $user = $getUserByIdQuery->fetch();

        return $user;
    }

    public static function getByToken($token)
    {
        $connection = getDatabaseConnection();
        $getUserByIdQuery = $connection->prepare("SELECT * FROM users WHERE token = :token;");
        $getUserByIdQuery->execute(["token" => $token]);
        
        return $getUserByIdQuery->fetch();
    }

    public static function updateById($json)
    {
        $allowedColumns = ["username", "password", "token","noPub","nbPub"];
        $columns = array_keys($json);
        $set = [];

        foreach ($columns as $column) {
            if (!in_array($column, $allowedColumns)) {
                continue;
            }

            $set[] = "$column = :$column";
        }

        $set = implode(", ", $set);
        $sql = "UPDATE users SET $set WHERE id = :id";
        $connection = getDatabaseConnection();
        $query = $connection->prepare($sql);
        $query->execute($json);
    }
}
