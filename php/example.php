<?php
// An example of using php-browserstacklocal

namespace BrowserStack;

use BrowserStack\BrowserStackLocal;
use BrowserStack\BrowserStackLocalException;

require_once('vendor/autoload.php');

$me = new BrowserStackLocal(getenv("BROWSERSTACK_KEY"));
$me->start();
$me->stop();
