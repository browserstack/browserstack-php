<?php
// An example of using php-browserstacklocal.


use BrowserStack\BrowserStackLocal;
require_once __DIR__ . '/../vendor/autoload.php';

$me = new BrowserStackLocal(getenv("BROWSERSTACK_KEY"));

$me->command();