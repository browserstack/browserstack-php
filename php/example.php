<?php
// An example of using php-browserstacklocal

namespace BrowserStack;

use BrowserStack\BrowserStackLocal;
use BrowserStack\BrowserStackLocalException;

require_once('vendor/autoload.php');

$me = new BrowserStackLocal(getenv("BROWSERSTACK_KEY"));
$me->set_local_identifier("hello1");
$me->start();
// $me1 = new BrowserStackLocal(getenv("BROWSERSTACK_KEY"));
// $me1->set_local_identifier("hello2");
// $me1->start();
sleep(30);
$me->stop();
// $me1->stop();
