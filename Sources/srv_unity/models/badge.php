<?php

require_once  __DIR__ . "/../library/get-database-connection.php";

class BadgeModel
{
    public static function getAll()
    {
        $connection = getDatabaseConnection();

        $getBadgesQuery = $connection->query("SELECT * FROM badge");

        $badges = $getBadgesQuery->fetchAll();

        return $badges;
    }

    public static function create($badge)
    {
        $connection = getDatabaseConnection();
        $createUserQuery = $connection->prepare("INSERT INTO badge(name, prix) VALUES(:name, :prix);");
        $createUserQuery->execute($badge);
    }


    public static function getAllBadge()
    {
        $connection = getDatabaseConnection();
        $getBadgeByIdQuery = $connection->prepare("SELECT * FROM badge;");
        $getBadgeByIdQuery->execute([]);

        return $getBadgeByIdQuery->fetch();
    }

    public static function getBadgeUser($badge)
    {
        $connection = getDatabaseConnection();
        $getBadgeByIdQuery = $connection->prepare("SELECT * FROM badge where id = :badgeID;");
        $getBadgeByIdQuery->execute([
            "badgeID" => $badge
        ]);

        return $getBadgeByIdQuery->fetchAll();
    }

    public static function getBadgeIdWithName($badge)
    {
        $connection = getDatabaseConnection();
        $getBadgeByIdQuery = $connection->prepare("SELECT id FROM badge WHERE name = :badgeName;");
        $getBadgeByIdQuery->execute([
            "badgeName" => $badge
        ]);

        return $getBadgeByIdQuery->fetchColumn(); // Retourner directement l'ID
    }

}
