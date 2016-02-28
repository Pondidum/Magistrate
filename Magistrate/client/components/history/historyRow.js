import React from 'react'

var HistoryRow = React.createClass({

  render() {

    var history = this.props.history;
    var current = this.props.current;

    var stamp = moment(history.at);
    var displayDate = current.diff(stamp, 'days') <= 1
      ? stamp.fromNow()
      : stamp.calendar();

    return (
      <li className="history-row col-md-12 tile">
        <div className="panel panel-default">
          <div className="panel-body">
            <div className="col-sm-3">{displayDate}</div>
            <div className="col-sm-9"><strong>{history.action}</strong> by <strong>{history.by.name}</strong>: {history.description}</div>
          </div>
        </div>
      </li>
    );
  }
});

export default HistoryRow
