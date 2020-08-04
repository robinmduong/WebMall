import React, { Component } from "react";
import { NavLink } from "react-router-dom";

class ScheduleButton extends Component {
  render() {
    return (
      <React.Fragment>
        <div className="schedule-button m-3">
          <NavLink
            to="/schedule/availability/add"
            name="schedule-button"
            id="schedule-button"
            className="btn btn-primary"
          >
            <i className="fa fa-plus mr-2"></i>
            Add Appointment
          </NavLink>
        </div>
      </React.Fragment>
    );
  }
}

export default ScheduleButton;
