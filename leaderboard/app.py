import json
from flask import Flask, request, jsonify

app = Flask(__name__)

@app.route('/get')
def get_leaderboard():
    with open('leaderboard.json', 'r') as f:
        data = f.read()
    if not data:
        leaderboard = []
    else:
        leaderboard = json.loads(data)
    return jsonify(leaderboard)

@app.route('/add', methods=['POST'])
def add_to_leaderboard():
    with open('leaderboard.json', 'r') as f:
        data = f.read()
    if not data:
        leaderboard = [request.form]
    else:
        leaderboard = json.loads(data)
        leaderboard.append(request.form)

    leaderboard.sort(key=sort_by_score, reverse=True)

    with open('leaderboard.json', 'w') as f:
        f.write(json.dumps(leaderboard))

    print(leaderboard)
    return jsonify(success=True), 201

def sort_by_score(item):
    return int(item['score'])
