import React from 'react'
import moment from 'moment'

const HistoryRow = ({ history, current }) => {

  var stamp = moment(history.stamp);
  var displayDate = current.diff(stamp, 'days') <= 1
    ? stamp.fromNow()
    : stamp.calendar();

  return (
    <li className="history-row col-md-12 tile">
      <div className="panel panel-default">
        <div className="panel-body">
          <div className="col-sm-3">{displayDate}</div>
          <div className="col-sm-9">{history.description}</div>
        </div>
      </div>
    </li>
  );
}

export default HistoryRow
