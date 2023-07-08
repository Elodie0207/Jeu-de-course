<?php

require __DIR__ . "/../../folder_library/json-response.php";
require __DIR__ . "/../../models/pub.php";
require __DIR__ . "/../../folder_library/request.php";

try {
    $json = Request::getJsonBody();

    PubModel::create($json);
    Response::json(201, [], ["success" => true]);
    die();

} catch (PDOException $exception) {
    $errorMessage = $exception->getMessage();
    Response::json(500, [], ["error" => "Error while accessing the database: $errorMessage"]);
}
?>
