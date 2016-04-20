<?php
require_once('/usr/webapps/common/jwt_helper.php');
require_once('/usr/webapps/common/idt_ldap.php');
 
if (!$_SERVER['REMOTE_USER']) exit;
list($uid, $domain) = explode('@', $_SERVER['REMOTE_USER'], 2);
setup_ldap();
$email = get_ldap_info($uid, ‘email’);
 
$key = 'abcdefg234859';  #replace with your secret key
$data = array();
$data['email'] = $email;
 
$token = array();
$token['data'] = $data;
$time = time();
$token['iat'] = $time;
$token['nbf'] = $time - 180;
$token['exp'] = $time + 300;
$token['aud'] = 'www.waggl.com';
 
$encoded = JWT::encode($token, $key, 'HS512');
 
$path = urldecode($_GET['return_to_path']);
$params = urldecode($_GET['return_to_params']);
$redir = "https://$host$path?sso_jwt=$encoded";
if(strlen($params)) {
    $redir .= '&' . $params;
}
header("Location: $redir");
?>