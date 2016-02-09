require 'rubygems'
require 'minitest/autorun'
require 'browserstack-local'

class BrowserStackLocalTest < Minitest::Test
  def setup
    @bs_local = BrowserStackLocal.new(ENV["BROWSERSTACK_KEY"])
  end

  def test_check_pid
    @bs_local.start
    refute_nil @bs_local.pid, 0
  end

  def test_multiple_binary
    @bs_local.start
    bs_local_2 = BrowserStackLocal.new(ENV["BROWSERSTACK_KEY"])
    assert_raises BrowserStackLocalException do 
      bs_local_2.start
    end
  end

  def test_enable_verbose
    @bs_local.enable_verbose
    assert_match /\-v/, @bs_local.command
  end

  def test_set_folder
    @bs_local.set_folder "/"
    assert_match /\-f/, @bs_local.command
    assert_match /\'\/\'/, @bs_local.command
  end

  def test_enable_force
    @bs_local.enable_force
    assert_match /\-force/, @bs_local.command
  end

  def test_enable_only
    @bs_local.enable_only
    assert_match /\-only/, @bs_local.command
  end

  def test_enable_only_automate
    @bs_local.enable_only_automate
    assert_match /\-onlyAutomate/, @bs_local.command
  end

  def test_enable_force_local
    @bs_local.enable_force_local
    assert_match /\-forcelocal/, @bs_local.command
  end

  def test_set_local_identifier
    @bs_local.set_local_identifier "randomString"
    assert_match /\-localIdentifier \'randomString\'/, @bs_local.command
  end

  def test_set_proxy
    @bs_local.set_proxy "localhost", 8080, "user", "pass"
    assert_match /\-proxyHost \'localhost\' \-proxyPort 8080 \-proxyUser \'user\' \-proxyPass \'pass\'/, @bs_local.command
  end

  def teardown
    @bs_local.stop
  end
end
