import axios from "axios";
import logger from "sabio-debug";
import * as serviceHelper from "./serviceHelpers";

const _logger = logger.extend("shoppingcartService");

const shoppingcartService = {
  endpoint: `${serviceHelper.API_HOST_PREFIX}/api/userdashboard/shoppingcart`,
};

shoppingcartService.get = () => {
  const config = {
    method: "GET",
    url: shoppingcartService.endpoint,
    data: null,
    crossdomain: true,
    headers: { "Content-Type": "application/json" },
  };

  _logger("shoppingcartService.get()", config);

  return axios(config)
    .then(serviceHelper.onGlobalSuccess)
    .catch(serviceHelper.onGlobalError);
};

export default shoppingcartService;
