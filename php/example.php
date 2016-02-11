<?php
// An example of using php-browserstacklocal

namespace BrowserStack;

use BrowserStack\BrowserStackLocal;
use BrowserStack\BrowserStackLocalException;

require_once('vendor/autoload.php');

$me = new BrowserStackLocal(getenv("BROWSERSTACK_KEY"));
$me->verbose();
$me->enable_folder();
$me->start();
$me->stop();
