import axios from "axios";
import logger from "sabio-debug";
import * as serviceHelper from "./serviceHelpers";

const userDashProductService = `${serviceHelper.API_HOST_PREFIX}/api/userdashboard/products/`;

const _logger = logger.extend("userDashProductService");

let getAll = () => {
  _logger("userDashProductService getAll is running!");
  const config = {
    method: "GET",
    url: `${userDashProductService}`,
    data: null,
    crossdomain: true,
    withCredentials: true,
    headers: { "Content-type": "application/json" },
  };

  return axios(config)
    .then(serviceHelper.onGlobalSuccess)
    .catch(serviceHelper.onGlobalError);
};

export { getAll };
