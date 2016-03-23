
# Waggl JWT Authentication

## Introduction

Waggl supports using JWT (JSON Web Tokens) as a Single Sign On (SSO) mechanism.  JWT is an industry standard method for representing claims securely between two parties.  You can learn more about JWT at [jwt.io](http://jwt.io/).

## Workflow

1. User clicks a Waggl link.
2. They are redirected to an SSO URL that you provide us.  When redirecting we will add a parameter that tells you where to send them after you authenticate.
3. They go to your URL and authenticate (using Active Directory or other internal authentication system).
4. After successful authentication you redirect them to the Waggl by taking the URL we have passed to you and adding a JWT Token which is unique to this user.
5. Waggl processes and verifies JWT Token which identifies the email for the user and logs them in.

## Processing Requests to your SSO URL

You will provide us a URL that we will redirect your users to for authentication.  For example, [https://yourcompany.com/waggl/sso/](https://yourcompany.com/waggl/sso/).  We will include 2 parameters - return\_to\_path and return\_to\_parameters.  When they come to your URL, you will do the following:

1. Authenticate them (if needed)
2. Generate JWT Token (see below)
3. Redirect them to the return\_to\_path url we provide with the JWT Token

## Generating JWT Token

See [http://jwt.io/#libraries](http://jwt.io/#libraries) to find a JWT library in the language of your choice.  We will provide you a secret key that is unique to your organization for signing your token.  We require the HS512 hash algorithm for signing. Your token should contain the following json data:

	{
	
		data: {
			email: email address of user
			}
		iat: current date/time  
		nbf: date/time when this token should not be usable before.  This should be 3 mins before the current time to allow for any differences is machine clocks.  
		exp: date/time when this token should expire.  After this time, the link will no longer work. We recommend that this is 5 mins after the current time.  
		aud: "www.waggl.com"
		
	}

Note: Dates should be the number of seconds since the Epoch (1970-01-01T00:00:00Z UTC).

## Generating Redirect

You create the redirect URL in the following way "https://app.waggl.com/\<return\_to\_path>?sso\_jwt=<jwt>&\<return\_to\_params>".  Note, return\_to\_params is an optional parameter that will only be included when needed.

For example, with the following values:

* return\_to\_path: "i/9745804b"
* return\_to\_params: "view=vote&page=1"
* jwt: "xxxxx.yyyyy.zzzzz"

Then the redirect URL would be https://app.waggl.com/i/9745804b?sso\_jwt=xxxxx.yyyyy.zzzzz&view=vote&page=1



Or if there is no return\_to\_params:

* return\_to\_path: "i/9745804b"

* jwt: "xxxxx.yyyyy.zzzzz"

Then the redirect URL would be: https://app.waggl.com/i/9745804b?sso\_jwt=xxxxx.yyyyy.zzzzz]

## Example Files

A Dot Net example is [here.](https://raw.githubusercontent.com/waggl/waggl_jwt_sso/master/example_code/dot_net.cs)

A Ruby on Rails example is [here.](https://raw.githubusercontent.com/waggl/waggl_jwt_sso/master/example_code/ssos_controller.rb)




