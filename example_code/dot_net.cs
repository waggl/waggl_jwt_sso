var secret_key = 'adskfjlksflksgjl'

DateTime now_ = DateTime.UtcNow;
DateTime EPoch = new DateTime(1970, 1, 1, 0, 0, 0);

int iat_ = Convert.ToInt32(now_.Subtract(EPoch).TotalSeconds);
int nbf_ = Convert.ToInt32(now_.AddMinutes(-3).Subtract(EPoch).TotalSeconds);
int exp_ = Convert.ToInt32(now_.AddMinutes(5).Subtract(EPoch).TotalSeconds);

var emailobject = new { email = currentuserEmail };

var payload = new
{
    data = emailobject,
    iat = iat_,
    nbf = nbf_,
    exp = exp_,
    aud = "www.waggl.com"
};

//using jose-jwt lib
string token = Jose.JWT.Encode(payload, Encoding.UTF8.GetBytes(secret_key), Jose.JwsAlgorithm.HS512);

string returntoPath = Convert.ToString(Request.QueryString["return_to_path"]);

string returntoparams = Convert.ToString(Request.QueryString["return_to_params"]);
 
string returntoparams = returntoParams == null ? "" : @"&" + returntoParams;
 
Response.Redirect(wagglURL + returntoPath + "?sso_jwt=" + token + returntoparams);
