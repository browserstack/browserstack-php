<?php
// An example of using php-browserstacklocal

namespace BrowserStack\BrowserStackLocal;

use BrowserStack\BrowserStackLocal;

require_once('vendor/autoload.php');

$me = new BrowserStackLocal('boring', '12345', 12345);


$me->command();