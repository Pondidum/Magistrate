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
      return (<HistoryRow key={index} history={item} current={current} />);
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
