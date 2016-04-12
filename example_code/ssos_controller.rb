class SsosController < ApplicationController
  SECRET_KEY = 'adskfjlksflksgjl' 
  WAGGL_SERVER = 'www.waggl.com'

  def create
    redirect_to redirect_url
  end

  private

  def redirect_url
    base = "#{WAGGL_SERVER + params[:return_to_path]}?sso_jwt=#{jwt_token}"
    if params[:return_to_params].present?
      base = "#{base}&#{params[:return_to_params]}"
    end
    base
  end

  def jwt_token
    payload = {
      data: {
        email: current_user.email
      },
      iat: Time.now.to_i,
      nbf: Time.now.to_i - 3 * 1.minute,
      exp: Time.now.to_i + 5 * 1.minute,
      aud: "www.waggl.com"
    }
    hmac_secret = SECRET_KEY
    JWT.encode payload, hmac_secret, 'HS512'
  end
end
