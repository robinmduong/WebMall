import axios from "axios";
import logger from "sabio-debug";
import * as serviceHelper from "./serviceHelpers";

const userDashEventService = `${serviceHelper.API_HOST_PREFIX}/api/userdashboard/events`;

const _logger = logger.extend("userDashEventService");

let getAll = () => {
  _logger("getAll is running!");
  const config = {
    method: "GET",
    url: `${userDashEventService}`,
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
