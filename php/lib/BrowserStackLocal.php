<?php

namespace BrowserStack;

use Exception;

class BrowserStackLocal {

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

 	public  function verbose() {
    	$this->verbose_flag = "-v";
  }

	public  function enable_folder($path) {
  	$this->folder_flag = "-f";
  	$this->folder_path = $path;
	}

  public  function enable_force() {
    $this->force_flag = "-force";
  }

  public  function enable_only() {
    $this->only_flag = "-only";
  }

  public  function enable_only_automate() {
    $this->only_automate_flag = "-onlyAutomate";
  }

  public  function enable_force_local() {
    $this->force_local_flag = "-forcelocal";
  }

  public  function set_local_identifier($localIdentifier) {
    $this->local_identifier_flag = "-localIdentifier $localIdentifier";
  }

  public  function start() {
  //   $process = IO.popen(command, "w+")

  //   while true
  //     line = $process.readline
  //     break if line.nil?
  //     if line.match(/\*\*\* Error\:/)
  //       $process.close
  //       raise BrowserStackLocalException.new(line)
  //       return
  //     }
  //     if line.strip == "Press Ctrl-C to exit"
  //       $pid = $process.pid
  //       return
  //     }
  //   }
  }

  public  function stop() {
  //   return if $pid.nil?
  //   Process.kill("INT", $pid)
  //   $process.close
  }

  public function command() {
    return "BrowserStackLocal $this->folder_flag $this->key $this->folder_path $this->force_local_flag $this->local_identifier_flag $this->only_flag $this->only_automate_flag $this->force_flag $this->verbose_flag";
  }
}
