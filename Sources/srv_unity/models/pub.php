<?php

require_once __DIR__ . "/../folder_library/get-database-connection.php";

class PubModel
{
    public static function getAll()
    {
        $connection = getDatabaseConnection();

        $getPubsQuery = $connection->query("SELECT * FROM pub");

        return $getPubsQuery->fetchAll();
    }
    public static function getAllBanniere()
    {
        $connection = getDatabaseConnection();

        $getPubsQuery = $connection->prepare("SELECT * FROM pub where isBanniere = :isBanniereParam");
        $getPubsQuery->execute([
            "isBanniereParam" => 1
        ]);
        return $getPubsQuery->fetchAll();
    }

    public static function getAllWithoutBannierre()
    {
        $connection = getDatabaseConnection();

        $getPubsQuery = $connection->prepare("SELECT * FROM pub where isBanniere = :isBanniereParam");
        $getPubsQuery->execute([
            "isBanniereParam" => 0
        ]);
        return $getPubsQuery->fetchAll();
    }

    public static function create($pub)
    {
        $connection = getDatabaseConnection();
        $createUserQuery = $connection->prepare("INSERT INTO pub(themeID, lien) VALUES(:theme, :lien);");
        $createUserQuery->execute($pub);
    }


    public static function getAllPub()
    {
        $connection = getDatabaseConnection();
        $getPubByIdQuery = $connection->prepare("SELECT * FROM pub;");
        $getPubByIdQuery->execute([]);

        return $getPubByIdQuery->fetch();
    }

    public static function getPubUser($theme)
    {
        //recup all pub depuis id depuis aime theme

        $connection = getDatabaseConnection();
        $getPubByIdQuery = $connection->prepare("SELECT * FROM pub where themeID = :themeID;");
        $getPubByIdQuery->execute([
            "themeID" => $theme
        ]);

        return $getPubByIdQuery->fetchAll();
    }

    public static function getPubUserBannierre($theme)
    {
        //recup all banniere depuis id depuis aime theme

        $connection = getDatabaseConnection();
        $getPubByIdQuery = $connection->prepare("SELECT * FROM pub where themeID = :themeID and isBanniere = :isBanniere");
        $getPubByIdQuery->execute([
            ":themeID" => $theme,
            ":isBanniere" => 1
        ]);

        return $getPubByIdQuery->fetchAll();
    }

    public static function getPubUserWithoutBannierre($theme)
    {
        //recup all pub depuis id depuis aime theme

        $connection = getDatabaseConnection();
        $getPubByIdQuery = $connection->prepare("SELECT * FROM pub where themeID = :themeID and isBanniere = :isBanniere");
        $getPubByIdQuery->execute([
            ":themeID" => $theme,
            ":isBanniere" => 0
        ]);

        return $getPubByIdQuery->fetchAll();
    }


    public static function getById($id)
    {
        $connection = getDatabaseConnection();
        $getUserByIdQuery = $connection->prepare("SELECT * FROM pub WHERE id = :id;");

        $getUserByIdQuery->execute(
            [
            "id" => $id
            ]
        );

        return $getUserByIdQuery->fetch();
    }



}
