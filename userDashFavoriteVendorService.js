import axios from "axios";
import logger from "sabio-debug";
import * as serviceHelper from "./serviceHelpers";

const userDashFavoriteVendorService = `${serviceHelper.API_HOST_PREFIX}/api/userdashboard/favoritevendors/`;

const _logger = logger.extend("userDashFavoriteVendorService");

let get = () => {
  _logger("userDashFavoriteVendorService getAll is running!");
  const config = {
    method: "GET",
    url: `${userDashFavoriteVendorService}`,
    data: null,
    crossdomain: true,
    withCredentials: true,
    headers: { "Content-type": "application/json" },
  };

  return axios(config)
    .then(serviceHelper.onGlobalSuccess)
    .catch(serviceHelper.onGlobalError);
};

export { get };
