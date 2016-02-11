<?php

namespace BrowserStack;

use Exception;

class BrowserStackLocal {

  private $handle = NULL;
  
	public function __construct($key) {
    $this->key = $key;
    $this->verbose_flag = "";
    $this->folder_flag = "";
    $this->folder_path = "";
    $this->force_flag = "";
    $this->only_flag = "";
    $this->only_automate_flag = "";
    $this->force_local_flag = "";
    $this->local_identifier_flag = "";
  }

	public function __destruct() {
        echo "";
  }

 	public function verbose() {
    	$this->verbose_flag = "-v";
  }

	public function enable_folder($path = "") {
  	$this->folder_flag = "-f";
  	$this->folder_path = $path;
	}

  public function enable_force() {
    $this->force_flag = "-force";
  }

  public function enable_only() {
    $this->only_flag = "-only";
  }

  public function enable_only_automate() {
    $this->only_automate_flag = "-onlyAutomate";
  }

  public function enable_force_local() {
    $this->force_local_flag = "-forcelocal";
  }

  public function set_local_identifier($localIdentifier = "") {
    $this->local_identifier_flag = "-localIdentifier $localIdentifier";
  }

  public function start() {
    $call = $this->command();    
    if ($this->is_windows()) {
       $this->handle = popen("start /b .$call.", "r");
    }
    else {
      $this->handle = popen("$call /dev/null &", "r");
    }  
    while(!feof($this->handle)) {
        $buffer = fgets($this->handle);
          
        if (preg_match("/\bError\b/i", $buffer,$match)) {
          throw new BrowserStackLocalException($buffer);
          pclose($this->handle);
          return;
        }
        elseif (strcmp(rtrim($buffer),"Press Ctrl-C to exit") == 0)
        {
          return;
        } 
        flush();    
    }
  }

  public function stop() {
    if (is_null($this->handle))
      return;
    else{
      pclose($this->handle);
    }
  }

  public function command() {
    return "./BrowserStackLocal $this->folder_flag $this->key $this->folder_path $this->force_local_flag $this->local_identifier_flag $this->only_flag $this->only_automate_flag $this->force_flag $this->verbose_flag";
 
  }

  private function is_windows(){
    if(PHP_OS == "WINNT" || PHP_OS == "WIN32"){
        return true;
    }
    return false;
  }
}

