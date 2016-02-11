<?php
// An example of using php-browserstacklocal.

namespace BrowserStack;

use BrowserStack\BrowserStackLocal;
use BrowserStack\BrowserStackLocalException;

require_once __DIR__ . '/../vendor/autoload.php';

class BrowserStackLocalTest extends \PHPUnit_Framework_TestCase {
	
  private $bs_local;
  
  public function setUp(){
    $this->bs_local = new BrowserStackLocal(getenv("BROWSERSTACK_KEY"));
  }
  
  public function test_multiple_binary() {
    $this->bs_local->start();
    $bs_local_2 = new BrowserStackLocal(getenv("BROWSERSTACK_KEY"));  
    try {
      $bs_local_2->start();
    } catch (BrowserStackLocalException $ex) {
        $emessage = $ex->getMessage();
        $this->assertEquals(trim($emessage), '*** Error: Either another browserstack local client is running on your machine or some server is listening on port 45691');
        $this->bs_local->stop();
        return;
      }
    $this->fail("Expected Exception has not been raised.");
    $this->bs_local->stop();
  }

  
  public function test_verbose() {
    $this->bs_local->verbose();
    $this->assertContains('-v',$this->bs_local->command());
  }

  public function test_enable_folder() {
    $this->bs_local->enable_folder("/");
    $this->assertContains('-f',$this->bs_local->command());
    $this->assertContains('/',$this->bs_local->command());
  }
  
  public function test_enable_force() {
    $this->bs_local->enable_force();
  }

  public function test_enable_only() {
    $this->bs_local->enable_only();
    $this->assertContains('-only',$this->bs_local->command());
  }

  public function test_enable_only_automate() {
    $this->bs_local->enable_only_automate();
    $this->assertContains('-onlyAutomate', $this->bs_local->command()); 
  }

  public function test_enable_force_local() {
    $this->bs_local->enable_force_local();
    $this->assertContains('-forcelocal',$this->bs_local->command());
  }

  public function test_set_local_identifier() {
    $this->bs_local->set_local_identifier("randomString");
    $this->assertContains('-localIdentifier randomString',$this->bs_local->command());
  }

}