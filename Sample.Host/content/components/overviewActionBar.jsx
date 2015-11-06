var OverviewActionBar = React.createClass({

  render() {

    return (
      <div className="row" style={{ marginBottom: "1em" }}>
        <div className="col-sm-7">

          <ul className="list-unstyled list-inline">
            {this.props.children}
          </ul>

        </div>
        <FilterBar className="pull-right col-sm-5" filterChanged={this.props.filterChanged} />
      </div>
    );

  }

});
