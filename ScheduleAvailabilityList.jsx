import React, { Component } from "react";
import logger from "sabio-debug";
import { withRouter } from "react-router-dom";
import * as availabilityService from "../../services/ScheduleAvailabilityService";
import SingleAvailability from "./SingleAvailability";
import ScheduleButton from "./ScheduleButton";
import PropTypes from "prop-types";
import Pagination from "rc-pagination";
import localInfo from "rc-pagination/lib/locale/en_US";
import "rc-pagination/assets/index.css";
import { toast } from "react-toastify";

const _logger = logger.extend("ScheduleAvailabilityList");

class ScheduleAvailabilityList extends Component {
  constructor(props) {
    super(props);

    this.state = {
      mappedAvailabilities: [],
      current: 1,
      pageIndex: 0,
      pageSize: 5,
      total: 0,
    };
  }

  componentDidMount() {
    _logger("componentDidMount is running!");
    this.getAllPaginated(0);
  }

  getAllPaginated = (pageIndex) => {
    availabilityService
      .paginate(pageIndex, this.state.pageSize)
      .then(this.onPaginateSuccess)
      .catch(this.onPaginateError);
  };

  onPaginateSuccess = (response) => {
    let availabilityArr = response.item.pagedItems;
    let mappedAvailabilities = availabilityArr.map(this.mapAvailabilities);
    this.setState(
      (prevState) => {
        return {
          ...prevState,
          mappedAvailabilities,
          total: response.item.totalCount,
        };
      },
      () => _logger(this.state.total)
    );
  };

  onPaginateError = (error) => {
    _logger(error);
  };

  onPageChange = (page) => {
    _logger("onPageChange is running", page);
    this.setState({
      current: page,
    });
    availabilityService
      .paginate(page - 1, this.state.pageSize)
      .then(this.onPaginateSuccess)
      .catch(this.onPaginateError);
  };

  mapAvailabilities = (anAvailability) => (
    <SingleAvailability
      availability={anAvailability}
      key={anAvailability.id}
      update={this.getAvailabilityData}
      delete={this.deleteAvailability}
    />
  );

  getAvailabilityData = (availability) => {
    this.props.history.push(
      `/schedule/availability/${availability.id}/edit`,
      availability
    );
  };

  deleteAvailability = (availability) => {
    availabilityService
      .deleteById(availability.id)
      .then(this.onDeleteSuccess(availability))
      .catch(this.onDeleteError);
  };

  onDeleteSuccess = (availability) => {
    toast.success("Schedule availability deleted!");

    this.setState((prevState) => {
      const availabilityIndex = prevState.mappedAvailabilities.findIndex(
        (singleAvailability) =>
          singleAvailability.props.availability.id === availability.id
      );

      const updatedAvailabilities = [...prevState.mappedAvailabilities];

      if (availabilityIndex >= 0) {
        updatedAvailabilities.splice(availabilityIndex, 1);
      }

      return {
        ...prevState,
        mappedAvailabilities: updatedAvailabilities,
      };
    }, this.stateChanged);
  };

  onDeleteError = (error) => {
    toast.error("Schedule availability not deleted.");
    _logger(error);
  };

  textItemRender = (current, type, element) => {
    if (type === "prev") {
      return "<";
    }
    if (type === "next") {
      return ">";
    }
    return element;
  };

  render() {
    return (
      <React.Fragment>
        <ScheduleButton />
        <div className="container-fluid">
          <div className="row">{this.state.mappedAvailabilities}</div>
        </div>

        <div className="rc-pagination">
          <Pagination
            onChange={this.onPageChange}
            current={this.state.current}
            pageIndex={this.state.pageIndex}
            pageSize={this.state.pageSize}
            total={this.state.total}
            locale={localInfo}
            itemRender={this.textItemRender}
          />
        </div>
      </React.Fragment>
    );
  }
}

ScheduleAvailabilityList.propTypes = {
  history: PropTypes.shape({
    push: PropTypes.func.isRequired,
  }),
};

export default withRouter(ScheduleAvailabilityList);
