import React from 'react'
import { connect } from 'react-redux'
import moment from 'moment'
import FilterBar from '../filterbar'

const mapStateToProps = (state) => {
  return {
    history: state.history;
  }
}
const HistoryOverview = ({ history }) => {

  var rows = history.map(function(item, index) {
    return (<HistoryRow key={index} history={item} current={current} index={index}/>);
  });//.slice(start, end);

  var links = [];

  return (
    <div>
      <div>
        <div className="row">
          <FilterBar filterChanged={() => { }} />
        </div>
        <div className="row">
          <ul className="list-unstyled col-sm-12">
            {rows}
          </ul>
        </div>
        <div className="row">
          <div className="col-sm-12 text-center">
            <ul className="pagination">
              {links}
            </ul>
          </div>
        </div>
      </div>
    </div>
  )
}

export default connect(mapStateToProps)(HistoryOverview)
/*
var HistoryOverview = React.createClass({

  getInitialState() {
    return {
      history: [],
      pageSize: 10,
      currentPage: 0
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

  onChangePage(index, e) {
    e.preventDefault();
    this.setState({ currentPage: index });
  },

  render() {
    var self = this;
    var current = moment();

    var totalPages = Math.floor(this.state.history.length / this.state.pageSize);
    var start = this.state.currentPage * this.state.pageSize;
    var end = start + this.state.pageSize;
    var links = [];

    var history = this.state.history.map(function(item, index) {
      return (<HistoryRow key={index} history={item} current={current} index={index}/>);
    }).slice(start, end);

    for (var i = 0; i < totalPages; i++) {
      links.push(<li key={i}><a href="#" onClick={self.onChangePage.bind(self, i)} >{i + 1}</a></li>);
    }

    return (
      <div>
        <div>
          <div className="row">
            <FilterBar filterChanged={this.onFilterChanged} />
          </div>
          <div className="row">
            <ul className="list-unstyled col-sm-12">
              {history}
            </ul>
          </div>
          <div className="row">
            <div className="col-sm-12 text-center">
              <ul className="pagination">
                {links}
              </ul>
            </div>
          </div>
        </div>
      </div>
    );
  }

});

export default HistoryOverview
*/
