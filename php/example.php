<?php
// An example of using php-browserstacklocal

namespace BrowserStack;

use BrowserStack\BrowserStackLocal;
use BrowserStack\BrowserStackLocalException;

require_once('vendor/autoload.php');

$me = new BrowserStackLocal(getenv("BROWSERSTACK_KEY"));
$me->add_args("localIdentifier", "yellow");
$me->add_args("v");
$me->start();
$me->stop();
