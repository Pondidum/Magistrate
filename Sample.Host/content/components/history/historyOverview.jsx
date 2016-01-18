var HistoryOverview = React.createClass({

  getInitialState() {
    return {
      history: []
    };
  },

  componentDidMount() {
    this.getHistory();
  },

  getHistory() {
    $.ajax({
      url: "/api/history/all",
      dataType: 'json',
      cache: false,
      success: function(data) {
        this.setState({
          history: data || []
        });
      }.bind(this)
    });
  },

  onFilterChanged(value) {
  },

  render() {
    var current = moment();

    var history = this.state.history.map(function(item, index) {

      var stamp = moment(item.at);
      var displayDate = current.diff(stamp, 'days') <= 1
        ? stamp.fromNow()
        : stamp.calendar();

      return (
        <li key={index} className="col-md-12 tile">
          <div className="panel panel-default">
            <div className="panel-body">
              <div className="col-sm-3">{displayDate}</div>
              <div className="col-sm-4"><strong>{item.action.replace(/Event$/, "")}</strong> by <strong>{item.by.name}</strong></div>
            </div>
          </div>
        </li>
      );
    });

    return (
      <div>
        <div>
          <div className="row">
            <FilterBar filterChanged={this.onFilterChanged} />
          </div>
          <ul className="list-unstyled col-sm-12">
            {history}
          </ul>
        </div>
      </div>
    );
  }

});
